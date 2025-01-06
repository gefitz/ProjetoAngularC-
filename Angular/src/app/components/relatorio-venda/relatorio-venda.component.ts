import { Component, OnInit, ViewChild } from '@angular/core';
import {MatTableModule,MatTableDataSource} from '@angular/material/table';
import{MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatSortModule} from '@angular/material/sort';
import { ApiService } from '../../Services/api.service';
import { VendaModel } from '../../Models/Venda.model';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import { FormGroup,FormControl,ReactiveFormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-relatorio-venda',
  standalone: true,
  imports: [MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatInputModule],
  templateUrl: './relatorio-venda.component.html',
  styleUrl: './relatorio-venda.component.css'
})

export class RelatorioVendaComponent implements OnInit {
  relatorioDataSource: MatTableDataSource<VendaModel> = new MatTableDataSource<VendaModel>;
  displayedColumns = ['nome','qtdVendida','vlrProduto','vlrTotal','dthVenda']
  pesquisaForm: FormGroup;
  _pipe!: DatePipe;
  @ViewChild(MatPaginator) paginator!:MatPaginator
  constructor(private api: ApiService, private pipe: DatePipe){
    this._pipe = pipe;
    this.relatorioDataSource.paginator = this.paginator; 
    this.relatorioDataSource.filterPredicate = (data,filter)=>{
      console.log("filter");
      return data.produto?.nome?.trim().toLowerCase().includes(filter)
    }
    this.pesquisaForm = new FormGroup({
      produto: new FormControl('')
    })
  }
  ngOnInit(): void {
    this.BuscaRelatorio();

  }
  ngAfterViewInit() {

  }
  BuscaRelatorio(){
    var filtro = this.pesquisaForm.value;
    console.log(filtro)
    this.api.post({endPoint:"Venda/BuscarTodasVendas?filtro="+ filtro}).subscribe({
      next:(data) => {
        if(data.sucesso){
          var relatorio = JSON.stringify(data.objeto);
          this.relatorioDataSource = JSON.parse(relatorio);

        }else{
          alert(data.mensagem);
        }
      },
      error:(data) =>{
        var ret = data.error;
        alert(ret);
      }
    })
  }
    ConvertOptionJson(produto:any){
      return JSON.stringify(produto);
  }
}
