import { Routes } from '@angular/router';
import { ProdutotelaComponent } from './components/produtotela/produtotela.component';
import { CadastrarProdutoComponent } from './components/cadastrar-produto/cadastrar-produto.component';
import { title } from 'process';
import { VendaComponent } from './components/venda/venda.component';

export const routes: Routes = [
    {
        path: "",
        component: ProdutotelaComponent,
        data:{title:"Lista Produtos"}
    },
    {
        path:"Produto/Cadastrar",
        component: CadastrarProdutoComponent,
        data: {title:"Cadastro Produto"}
    },
    {
        path: "Produto/Editar",
        component:CadastrarProdutoComponent,
        data:{title:"Editar Produto"}
    },
    {
        path:"Vendas/PDV",
        component: VendaComponent,
        data:{title:"Realizar Venda"}
    }
];
