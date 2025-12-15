import { useEffect, useState } from "react";
import { Table } from "reactstrap";
import { api } from "../../service/api";
import { RelatorioPessoas } from "../../models/Pessoa";
import "./RelatoriosPage.css";

export default function RelatorioPessoasPage() {
  const [relatorio, setRelatorio] = useState<RelatorioPessoas | null>(null);

  useEffect(() => {
    carregarRelatorio();
  }, []);

  async function carregarRelatorio() {
    try {
      const response = await api.get<RelatorioPessoas>("/pessoas/RelatorioDespesasPessoas");
      setRelatorio(response.data);
    } catch (error: any) {
      alert(error.response?.data || "Erro ao buscar relatório");
    }
  }

  return (
    <div className="relatorio-container">
      <h2>Relatório de Pessoas</h2>
      {relatorio && (
        <Table striped className="mt-3">
          <thead>
            <tr>
              <th>Nome</th>
              <th>Total Receitas</th>
              <th>Total Despesas</th>
              <th>Saldo</th>
            </tr>
          </thead>
          <tbody>
            {relatorio.pessoas.map(p => (
              <tr key={p.pessoaId}>
                <td>{p.nome}</td>
                <td>R$ {p.totalReceitas.toFixed(2)}</td>
                <td>R$ {p.totalDespesas.toFixed(2)}</td>
                <td>R$ {p.saldo.toFixed(2)}</td>
              </tr>
            ))}
            <tr className="total-row">
              <td><strong>Total Geral</strong></td>
              <td>R$ {relatorio.totalReceitasGeral.toFixed(2)}</td>
              <td>R$ {relatorio.totalDespesasGeral.toFixed(2)}</td>
              <td>R$ {relatorio.saldoGeral.toFixed(2)}</td>
            </tr>
          </tbody>
        </Table>
      )}
    </div>
  );
}
