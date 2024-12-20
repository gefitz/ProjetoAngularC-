import { Component } from '@angular/core';
import { ProdutoModel } from '../../Models/Produto.model';
import { ApiService } from '../../Services/api.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-produtotela',
  standalone: true,
  imports: [],
  templateUrl: './produtotela.component.html',
  styleUrl: './produtotela.component.css'
})
export class ProdutotelaComponent {
  produtoList!: ProdutoModel[];
  _pipe!: DatePipe;
  constructor(private api:ApiService, private pipe:DatePipe){
    this.BuscarTpProduto();
    this._pipe = pipe;
  }

  DeletarTpProduto(tpProduto: ProdutoModel){
    if(window.confirm("Você deseja realmente deletar esse produto?")){

      this.api.delete({endPoint:"Produtos",data:tpProduto}).subscribe({
        next: (data) =>{
          console.log(data);
        }});
      }
  }
  BuscarTpProduto(){
    this.api.get({endPoint:'Produtos/BuscarTodos'}).subscribe({
      next: (data) =>{
          if(data.sucesso){
            var ret = JSON.stringify(data.objeto);
            this.produtoList = JSON.parse(ret);
            console.log(this.produtoList);
          }else{
            console.log(data.menssagem);
          }
        }
    })
  }

}
