export interface Categoria {
  categoriaId: number;
  descricao: string;
  finalidade: number; 
}

export interface CategoriaDespesas {
  categoriaId: number;
  descricao: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

export interface RelatorioCategorias {
  categorias: CategoriaDespesas[];
  totalReceitasGeral: number;
  totalDespesasGeral: number;
  saldoGeral: number;
}
