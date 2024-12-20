import { Component } from '@angular/core';
import { ProdutoModel } from '../../Models/Produto.model';
import { ApiService } from '../../Services/api.service';
import { DatePipe } from '@angular/common';
import { RouterLink,Router } from '@angular/router';
import { FormGroup,FormControl,ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-produtotela',
  standalone: true,
  imports: [RouterLink,ReactiveFormsModule],
  templateUrl: './produtotela.component.html',
  styleUrl: './produtotela.component.css'
})
export class ProdutotelaComponent {
  buscarProduto: FormGroup;
  produtoList!: ProdutoModel[];
  _pipe!: DatePipe;
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
    this.router.navigateByUrl("/Produto/Cadastrar", {state: {produto}})
  }
  BuscarProduto(){
    if(this.buscarProduto){
      var produto = this.buscarProduto.value;
      console.log(produto);
      this.api.post({endPoint:'Produtos/buscaProduto',data:produto}).subscribe({
        next: (data) => {
          if(data.sucesso){
            var ret = JSON.stringify(data.objeto);
            this.produtoList = JSON.parse(ret);
          }else{
            console.log(data.mensagem);
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
            console.log(data.mensagem);
          }
        }
      })
    }
  }
  

}
