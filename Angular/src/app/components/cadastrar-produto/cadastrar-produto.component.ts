import { Component } from '@angular/core';
import { FormBuilder, FormControl,FormGroup, Validators,ReactiveFormsModule  } from "@angular/forms"
import { CommonModule, DatePipe } from '@angular/common';
import { ProdutoModel } from '../../Models/Produto.model';
import { ApiService } from '../../Services/api.service';
import { Route } from '@angular/router';
import { Router } from '@angular/router';
@Component({
  selector: 'app-cadastrar-produto',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './cadastrar-produto.component.html',
  styleUrl: './cadastrar-produto.component.css'
})
export class CadastrarProdutoComponent {
  produtoForm: FormGroup;
  model!: ProdutoModel
  constructor(private api: ApiService, private router: Router, private pipe: DatePipe) {
    this.produtoForm = this.getFormCadastro(this.model)
  }

  salvarProduto(): void {
    if (this.produtoForm.valid) {
      var produto = this.produtoForm.value;
      produto.dthCriacaoProduto = this.pipe.transform(produto.dthCriacaoProduto, 'yyyy/MM/ddThh:mm:ssZ');
      produto.dthAlteracaoProduto = this.pipe.transform(produto.dthAlteracaoProduto, 'yyyy/MM/ddThh:mm:ssZ');
      console.log(produto);
      this.api.post({endPoint:"Produtos",data:produto}).subscribe({
        next:(data) =>{
          if(data.sucesso){
            alert("Cadastrado com sucesso");
            this.router.navigate(['']);
          }
        }
      })
    } else {
      console.log('Formulário inválido');
    }
  }
  getFormCadastro(cadastro: ProdutoModel): FormGroup{
        
    if(cadastro){
        return new FormGroup({
          idProduto:new FormControl(cadastro.idProduto,Validators.required),
          nome: new FormControl(cadastro.nome,Validators.required),
          descricao: new FormControl(cadastro.descricao),
          tpProduto: new FormControl(cadastro.tpProduto,Validators.required),
          qtdProduto:new FormControl(cadastro.qtdProduto,Validators.required),
          vlrProduto:new FormControl(cadastro.vlrProduto,Validators.required),
          dthAlteracaoProduto:new FormControl(cadastro.dthAlteracaoProduto,Validators.required),
          dthCriacaoProduto:new FormControl(cadastro.dthCriacaoProduto,Validators.required),
        })
      } else {
        return new FormGroup({
          nome: new FormControl('', Validators.required),
          descricao: new FormControl(''),
          tpProduto: new FormControl('', Validators.required),
          qtdProduto: new FormControl('', Validators.required),
          vlrProduto: new FormControl('', Validators.required),
          dthAlteracaoProduto: new FormControl(new Date, Validators.required),
          dthCriacaoProduto: new FormControl(new Date, Validators.required),
      })
    };
  }
}
