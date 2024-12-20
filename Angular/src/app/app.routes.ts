import { Routes } from '@angular/router';
import { ProdutotelaComponent } from './components/produtotela/produtotela.component';
import { CadastrarProdutoComponent } from './components/cadastrar-produto/cadastrar-produto.component';

export const routes: Routes = [
    {
        path: "",
        component: ProdutotelaComponent,
    },
    {
        path:"Produto/Cadastrar",
        component: CadastrarProdutoComponent
    }

];
