import { ProdutoModel } from "./Produto.model";

export interface VendaModel{
    idVenda:number;
    produto: ProdutoModel;
    qtdProdutoVendido: number;
    vlrTotal:number;
    dthVenda:Date;
}