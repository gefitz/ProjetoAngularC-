import { DatePipe } from "@angular/common";

export interface ProdutoModel{
    idProduto: number;
    nome: string;
    descricao:string;
    tpProduto: string;
    qtdProduto: number;
    vlrProduto: number;
    dthCriacaoProduto: Date;
    dthAlteracaoProduto: Date;
}