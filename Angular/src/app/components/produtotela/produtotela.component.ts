import { Component, ViewChild } from '@angular/core';
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
export class ProdutotelaComponent{
  buscarProduto: FormGroup;
  produtoList!: MatTableDataSource<ProdutoModel>;
  _pipe!: DatePipe;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  displayedColumns = ['nome','descricao','qtdProduto','vlrProduto','dthAlteracao','btnEditar','btnDelete']
  constructor(private api:ApiService, private pipe:DatePipe, private router: Router){
    this.BuscarProduto();
    this._pipe = pipe;
    this.buscarProduto = new FormGroup({
      nome: new FormControl(''),
      tpProduto: new FormControl('')
    })
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
    if(this.buscarProduto){
      var produto = this.buscarProduto.value;
      this.api.post({endPoint:'Produtos/buscaProduto',data:produto}).subscribe({
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
    }else{

      this.api.get({endPoint:'Produtos/BuscarTodos'}).subscribe({
        next: (data) =>{
          if(data.sucesso){
            var ret = JSON.stringify(data.objeto);
            this.produtoList = JSON.parse(ret);
          }else{
            alert(data.mensagem);
          }
        }
      })
    }
  }
  ngAfterViewInit() {
    this.produtoList.paginator = this.paginator;
    this.produtoList.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.produtoList.filter = filterValue.trim().toLowerCase();

    if (this.produtoList.paginator) {
      this.produtoList.paginator.firstPage();
    }
  }

}
