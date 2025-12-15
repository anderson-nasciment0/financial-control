export interface Transacao {
  transacaoId: number;
  descricao: string;
  valor: number;
  tipoTransacao: number;
  pessoaId: number;
  categoriaId: number;
}
