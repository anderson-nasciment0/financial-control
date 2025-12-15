import { useEffect, useState } from "react";
import { Table } from "reactstrap";
import { api } from "../../service/api";
import { RelatorioCategorias } from "../../models/Categoria";
import "./RelatoriosPage.css";

export default function RelatorioCategoriasPage() {
  const [relatorio, setRelatorio] = useState<RelatorioCategorias | null>(null);

  useEffect(() => {
    carregarRelatorio();
  }, []);

  async function carregarRelatorio() {
    try {
      const response = await api.get<RelatorioCategorias>("/categorias/RelatorioDespesasCategorias");
      setRelatorio(response.data);
    } catch (error: any) {
      alert(error.response?.data || "Erro ao buscar relatório");
    }
  }

  function textoFinalidade(valor: number) {
    switch (valor) {
      case 1: return "Despesa";
      case 2: return "Receita";
      case 3: return "Ambas";
      default: return "";
    }
  }

  return (
    <div className="relatorio-container">
      <h2>Relatório de Categorias</h2>
      {relatorio && (
        <Table striped className="mt-3">
          <thead>
            <tr>
              <th>Descrição</th>
              <th>Total Receitas</th>
              <th>Total Despesas</th>
              <th>Saldo</th>
            </tr>
          </thead>
          <tbody>
            {relatorio.categorias.map(c => (
              <tr key={c.categoriaId}>
                <td>{c.descricao}</td>
                <td>R$ {c.totalReceitas.toFixed(2)}</td>
                <td>R$ {c.totalDespesas.toFixed(2)}</td>
                <td>R$ {c.saldo.toFixed(2)}</td>
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
