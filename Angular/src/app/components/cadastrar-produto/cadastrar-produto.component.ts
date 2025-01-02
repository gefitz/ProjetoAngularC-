import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormControl,FormGroup, Validators,ReactiveFormsModule  } from "@angular/forms"
import { ProdutoModel } from '../../Models/Produto.model';
import { ApiService } from '../../Services/api.service';
import { Router,RouterLink } from '@angular/router';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
@Component({
  selector: 'app-cadastrar-produto',
  standalone: true,
  imports: [ReactiveFormsModule,RouterLink,MatFormFieldModule,MatInputModule,MatButtonModule],
  templateUrl: './cadastrar-produto.component.html',
  styleUrl: './cadastrar-produto.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CadastrarProdutoComponent {
  produtoForm: FormGroup;
  model!: ProdutoModel
  constructor(private api: ApiService, private router: Router) {
    const nav = router.getCurrentNavigation();
    this.model = nav?.extras.state?.['produto'];
    this.produtoForm = this.getFormCadastro(this.model)
  }

  salvarProduto(): void {
    if (this.produtoForm.valid) {
      var produto = this.produtoForm.value;
      if(produto.idProduto == 0){
        //Cadastra novo produto
        this.api.post({endPoint:"Produtos",data:produto}).subscribe({
          next:(data) =>{
            if(data.sucesso){
              alert("Cadastrado com sucesso");
              this.router.navigate(['']);
            }else{
              alert(data.mensagem);
            }
          }
        })
      }else{
        //faz update
        this.model.nome = produto.nome;
        this.model.descricao = produto.descricao;
        this.model.tpProduto = produto.tpProduto;
        this.model.qtdProduto = produto.qtdProduto;
        this.model.vlrProduto = produto.vlrProduto;

        this.api.put({endPoint:"Produtos",data:this.model}).subscribe({
          next: (data) => {
            if(data.sucesso){
              alert("Produto Alterado com sucesso");
              this.router.navigate([''])
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
    } else {
      alert('Formulário inválido');
    }
  }
  getFormCadastro(cadastro: ProdutoModel): FormGroup{
    if(cadastro){
        return new FormGroup({
          idProduto: new FormControl(cadastro.idProduto,Validators.required),
          nome: new FormControl(cadastro.nome,Validators.required),
          descricao: new FormControl(cadastro.descricao),
          tpProduto: new FormControl(cadastro.tpProduto,Validators.required),
          qtdProduto:new FormControl(cadastro.qtdProduto,Validators.required),
          vlrProduto:new FormControl(cadastro.vlrProduto,Validators.required),
        })
      } else {
        return new FormGroup({
          idProduto: new FormControl(0, Validators.required),
          nome: new FormControl('', Validators.required),
          descricao: new FormControl(''),
          tpProduto: new FormControl('', Validators.required),
          qtdProduto: new FormControl('', Validators.required),
          vlrProduto: new FormControl('', Validators.required),
      })
    };
  }
}
