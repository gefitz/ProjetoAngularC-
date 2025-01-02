import { ProdutoModel } from "./Produto.model";

export interface VendaModel{
    idVenda:number;
    produto: ProdutoModel;
    nomeProduto: string;
    qtdProdutoVendido: number;
    vlrTotal:number;
}