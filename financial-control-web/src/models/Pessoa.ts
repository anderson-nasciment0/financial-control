export interface Pessoa {
  pessoaId: number;
  nome: string;
  idade: number;
}

export interface PessoaRelatorio {
  pessoaId: number;
  nome: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

export interface RelatorioPessoas {
  pessoas: PessoaRelatorio[];
  totalReceitasGeral: number;
  totalDespesasGeral: number;
  saldoGeral: number;
}