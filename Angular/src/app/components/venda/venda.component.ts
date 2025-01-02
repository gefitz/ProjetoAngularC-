import { Component, OnInit } from '@angular/core';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input'
import {MatButtonModule} from '@angular/material/button';
import {MatSelectModule} from '@angular/material/select';
import { FormControl, FormGroup,ReactiveFormsModule, Validators } from '@angular/forms';
import { ProdutoModel } from '../../Models/Produto.model';
import { ApiService } from '../../Services/api.service';
import { MatTableModule,MatTableDataSource} from '@angular/material/table';
import {MatIconModule} from '@angular/material/icon';
import {MatSort, MatSortModule} from '@angular/material/sort';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import { VendaModel } from '../../Models/Venda.model';
import { json } from 'stream/consumers';

@Component({
  selector: 'app-venda',
  standalone: true,
  imports: [MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    ReactiveFormsModule,
    MatTableModule,
    MatIconModule,
    MatSortModule,
    MatPaginatorModule
  ],
  templateUrl: './venda.component.html',
  styleUrl: './venda.component.css'
})
export class VendaComponent implements OnInit{
  vendaForm!: FormGroup;
  produtoList!: ProdutoModel[]
  tableVenda: MatTableDataSource<VendaModel> = new MatTableDataSource<VendaModel>;
  vendaList: VendaModel[] = [];
  vlrProduto: number = 0;
  disabled:boolean =true;
  vlrTotal:number = 0;
  displayedColumns=['nomeProduto','qtdProduto','vlrProduto','vlrTotal','btnDelete']
  constructor (private api: ApiService){}

  //Inicializador da pagina
  ngOnInit(): void { 

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

    this.vendaForm = new FormGroup({
      produto: new FormControl('',Validators.required),
      qtdProdutoVendido: new FormControl('',Validators.required),
      vlrProduto: new FormControl('',Validators.required),
      vlrTotal: new FormControl('',Validators.required)
    })
  }
salvarProduto() {
  if(this.vendaForm.valid){
    //Necessario pois se fazer parse e for recolocar o produto ele vira ja com parse,a parti disso n consigo verefica se ja existe na vendaList
    var venda = JSON.parse(JSON.stringify(this.vendaForm.value));
    try{
      venda.produto = JSON.parse(venda.produto);

    }catch(error){ console.log(error);}
    if(parseInt(venda.qtdProdutoVendido) > parseInt(venda.produto.qtdProduto)){
      alert("Quantidade superior ao do estoque");
      return;
    }
    var produtoFilter = this.vendaList.find(produto => produto.produto.idProduto === venda.produto.idProduto);
    
    if(produtoFilter){
        produtoFilter.qtdProdutoVendido = venda.qtdProdutoVendido;
    }else{
      this.vendaList.push(venda);
    }
    this.tableVenda.data = [... this.vendaList];
  }
  this.disabled = false;
}
//Atribui o valor do valor do produto no formGroup
ResgataValorProduto(produto:any){
  produto = JSON.parse(produto);
 this.vlrProduto = produto.vlrProduto!;
 this.vendaForm.patchValue({
  vlrProduto: this.vlrProduto
 })
}

//Calcula o total do produtos escolhido
CacularTotal(qtdProduto:Event){
  var qtd = (qtdProduto.target as HTMLInputElement).value
  this.vlrTotal = parseInt(qtd) * this.vlrProduto;
  this.vendaForm.patchValue({
    vlrTotal: this.vlrTotal
   })
}

RemoverVenda(produto:any){
  const index = this.vendaList.findIndex(venda => venda === produto);
  if (index !== -1) {
    this.vendaList.splice(index, 1);
    this.tableVenda.data = [... this.vendaList];
    if(this.vendaList.length === 0)
      this.disabled = true;
  }
}
  FinalizarVenda(){
    console.log(JSON.stringify(this.vendaList));
    this.api.post({endPoint:"Venda", data:this.vendaList}).subscribe({
      next: (data) => {
        if(data.sucesso){
          alert("Venda registrada com sucesso");
        }else{
          alert(data.mensagem);
        }
      },
      error: (data) => {
        alert(data)
      }
    })
  }
  ConvertOptionJson(produto:any){
    return JSON.stringify(produto);
  }
}
