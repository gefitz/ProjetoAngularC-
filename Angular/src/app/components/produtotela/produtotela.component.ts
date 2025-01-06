import { Component, OnInit, ViewChild } from '@angular/core';
import { ProdutoModel } from '../../Models/Produto.model';
import { ApiService } from '../../Services/api.service';
import { DatePipe } from '@angular/common';
import {Router } from '@angular/router';
import { FormGroup,FormControl,ReactiveFormsModule } from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatTableModule,MatTableDataSource} from '@angular/material/table';
import {MatSort, MatSortModule} from '@angular/material/sort';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatIconModule} from '@angular/material/icon'
@Component({
  selector: 'app-produtotela',
  standalone: true,
  imports: [ReactiveFormsModule, 
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    MatPaginatorModule,
    MatIconModule
    ],
  templateUrl: './produtotela.component.html',
  styleUrl: './produtotela.component.css'
})
export class ProdutotelaComponent implements OnInit{
  buscarProduto: FormGroup;
  produtoList: MatTableDataSource<ProdutoModel> = new MatTableDataSource<ProdutoModel>;
  _pipe!: DatePipe;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  displayedColumns = ['nome','descricao','qtdProduto','vlrProduto','dthAlteracao','btnEditar','btnDelete']
  constructor(private api:ApiService, private pipe:DatePipe, private router: Router){
    this._pipe = pipe;
    this.buscarProduto = new FormGroup({
      nome: new FormControl(''),
      tpProduto: new FormControl('')
    })
  }
  ngOnInit(): void {

    this.BuscarProduto();
    this.produtoList.paginator = this.paginator;
    this.produtoList.filterPredicate = (data,filter:string) =>{
      return data.nome.trim().toLowerCase().includes(filter) || data.tpProduto.trim().toLowerCase().includes(filter)
    }

  }

  DeletarTpProduto(produto: ProdutoModel){
    console.log(produto);
    if(window.confirm("Você deseja realmente deletar esse produto?")){

      this.api.delete({endPoint:"Produtos",data:produto}).subscribe({
        next: (data) =>{
          if(data.sucesso){
            alert("Produto deletado com sucesso");
            window.location.reload();
          }else{
            alert(data.mensagem);
          }
        },
        error: (data) =>{
          var ret = data.error;
          if(ret.sucesso){

            alert(ret.mensagem)
          }else{
            alert(ret.message)
          }
        }

      });
      }
  }
  EditarProduto(produto:ProdutoModel){
    this.router.navigateByUrl("/Produto/Editar", {state: {produto}})
  }
  BuscarProduto(){
      
      this.api.post({endPoint:'Produtos/buscaProduto',data:this.buscarProduto.value}).subscribe({
        next: (data) => {
          if(data.sucesso){
            var ret = JSON.stringify(data.objeto);
            this.produtoList = new MatTableDataSource(JSON.parse(ret));
          }else{
            alert(data.mensagem);
          }
        },
        error: (data) =>{
          var ret = data.error;
          if(ret.sucesso){

            alert(ret.mensagem)
          }else{
            alert(ret.message)
          }
        }

      })
  }

 //Filtro de pesquisa da tabela 
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.produtoList.filter = filterValue.trim().toLowerCase();

    if (this.produtoList.paginator) {
      this.produtoList.paginator.firstPage();
    }
  }

}
