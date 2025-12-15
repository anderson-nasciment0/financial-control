import { useEffect, useState } from "react";
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Table, Input, Label } from "reactstrap";
import { api } from "../../service/api";
import { Transacao } from "../../models/Transacao";
import { Pessoa } from "../../models/Pessoa";
import { Categoria } from "../../models/Categoria";
import "./TransacoesPage.css";

export default function TransacoesPage() {
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [categorias, setCategorias] = useState<Categoria[]>([]);

  const [transacaoSelecionada, setTransacaoSelecionada] = useState<Transacao | null>(null);

  const [modalCreate, setModalCreate] = useState(false);
  const [modalEdit, setModalEdit] = useState(false);
  const [modalDelete, setModalDelete] = useState(false);

  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState<number>(0);
  const [tipoTransacao, setTipoTransacao] = useState<number>(0);
  const [pessoaId, setPessoaId] = useState<number>(0);
  const [categoriaId, setCategoriaId] = useState<number>(0);

  useEffect(() => {
    carregarDados();
  }, []);

  async function carregarDados() {
    const [t, p, c] = await Promise.all([
      api.get<Transacao[]>("/transacoes"),
      api.get<Pessoa[]>("/pessoas"),
      api.get<Categoria[]>("/categorias")
    ]);

    setTransacoes(t.data);
    setPessoas(p.data);
    setCategorias(c.data);
  }

  //função para criar
  async function criarTransacao() {
    try{
    await api.post("/transacoes", {
      descricao,
      valor,
      tipoTransacao,
      pessoaId,
      categoriaId
    });
    setModalCreate(false);
    carregarDados();
  } catch(error : any){
    if(error.response){
      alert(error.response.data);
    }else{
      alert("Erro inesperado")
    }
  }
}

  //função para abrir modal do editar
  function abrirEditar(t: Transacao) {
    setTransacaoSelecionada(t);
    setDescricao(t.descricao);
    setValor(t.valor);
    setTipoTransacao(t.tipoTransacao);
    setPessoaId(t.pessoaId);
    setCategoriaId(t.categoriaId);
    setModalEdit(true);
  }

  //função para editar
  async function editarTransacao() {
    try{
    await api.put(`/transacoes/${transacaoSelecionada?.transacaoId}`, {
      transacaoId: transacaoSelecionada?.transacaoId,
      descricao,
      valor,
      tipoTransacao,
      pessoaId,
      categoriaId
    });
    setModalEdit(false);
    carregarDados();
  }
    catch(error : any){
    if(error.response){
      alert(error.response.data);
    }else{
      alert("Erro inesperado")
    }
  }
}

  //função para abrir modal do excluir
  function abrirExcluir(t: Transacao) {
    setTransacaoSelecionada(t);
    setModalDelete(true);
  }

  //função para excluir
  async function excluirTransacao() {
    await api.delete(`/transacoes/${transacaoSelecionada?.transacaoId}`);
    setModalDelete(false);
    carregarDados();
  }

  //seleção do tipo de transação
  function textoTipo(tipo: number) {
    return tipo === 1 ? "Despesa" : "Receita";
  }

  function nomePessoa(pessoaId: number) {
    const pessoa = pessoas.find(p => p.pessoaId === pessoaId);
    return pessoa ? pessoa.nome : "-";
  }

  function nomeCategoria(categoriaId: number) {
    const categoria = categorias.find(c => c.categoriaId === categoriaId);
    return categoria ? categoria.descricao : "-";
  }

  return (
    <div className="transacoes-container">
      <h2>Transações</h2>

      <Button color="success" onClick={() => setModalCreate(true)}>
        Nova Transação
      </Button>

      <Table striped className="mt-3">
        <thead>
          <tr>
            <th>Pessoa</th>
            <th>Categoria</th>
            <th>Descrição</th>
            <th>Valor</th>
            <th>Tipo</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {transacoes.map(t => (
            <tr key={t.transacaoId}>
              <td>{nomePessoa(t.pessoaId)}</td>
              <td>{nomeCategoria(t.categoriaId)}</td>
              <td>{t.descricao}</td>
              <td>R$ {t.valor.toFixed(2)}</td>
              <td>{textoTipo(t.tipoTransacao)}</td>
              <td>
                <Button size="sm" color="warning" onClick={() => abrirEditar(t)}>
                  Editar
                </Button>{" "}
                <Button size="sm" color="danger" onClick={() => abrirExcluir(t)}>
                  Excluir
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>

      <Modal isOpen={modalCreate} toggle={() => setModalCreate(false)}>
        <ModalHeader>Nova Transação</ModalHeader>
        <ModalBody>
          <Label>Descrição</Label>
          <Input value={descricao} onChange={e => setDescricao(e.target.value)} />

          <Label className="mt-2">Valor</Label>
          <Input type="number" value={valor} onChange={e => setValor(Number(e.target.value))} />

          <Label className="mt-2">Tipo</Label>
          <Input type="select" value={tipoTransacao} onChange={e => setTipoTransacao(Number(e.target.value))}>
            <option value={1}>Despesa</option>
            <option value={2}>Receita</option>
          </Input>

          <Label className="mt-2">Pessoa</Label>
          <Input type="select" value={pessoaId} onChange={e => setPessoaId(Number(e.target.value))}>
            <option value={0}>Selecione</option>
            {pessoas.map(p => (
              <option key={p.pessoaId} value={p.pessoaId}>{p.nome}</option>
            ))}
          </Input>

          <Label className="mt-2">Categoria</Label>
          <Input type="select" value={categoriaId} onChange={e => setCategoriaId(Number(e.target.value))}>
            <option value={0}>Selecione</option>
            {categorias.map(c => (
              <option key={c.categoriaId} value={c.categoriaId}>{c.descricao}</option>
            ))}
          </Input>
        </ModalBody>
        <ModalFooter>
          <Button color="primary" onClick={criarTransacao}>Salvar</Button>
          <Button color="secondary" onClick={() => setModalCreate(false)}>Cancelar</Button>
        </ModalFooter>
      </Modal>

      <Modal isOpen={modalEdit} toggle={() => setModalEdit(false)}>
        <ModalHeader>Editar Transação</ModalHeader>
        <ModalBody>
          <Label>Pessoa</Label>
          <Input type="select" value={pessoaId} onChange={e => setPessoaId(Number(e.target.value))}>
            <option value={0}>Selecione</option>
            {pessoas.map(p => (
              <option key={p.pessoaId} value={p.pessoaId}> {p.nome} </option>
            ))}
          </Input>

          <Label className="mt-2">Categoria</Label>
          <Input type="select" value={categoriaId} onChange={e => setCategoriaId(Number(e.target.value))}>
            <option value={0}>Selecione</option>
            {categorias.map(c => (
              <option key={c.categoriaId} value={c.categoriaId}>{c.descricao}</option>
            ))}
          </Input>

          <Label className="mt-2">Descrição</Label>
          <Input value={descricao} onChange={e => setDescricao(e.target.value)} />

          <Label className="mt-2">Valor</Label>
          <Input type="number" value={valor} onChange={e => setValor(Number(e.target.value))} />

          <Label className="mt-2">Tipo</Label>
          <Input type="select" value={tipoTransacao} onChange={e => setTipoTransacao(Number(e.target.value))}>
            <option value={1}>Despesa</option>
            <option value={2}>Receita</option>
          </Input>
        </ModalBody>
        <ModalFooter>
          <Button color="warning" onClick={editarTransacao}>Atualizar</Button>
          <Button color="secondary" onClick={() => setModalEdit(false)}>Cancelar</Button>
        </ModalFooter>
      </Modal>

      <Modal isOpen={modalDelete} toggle={() => setModalDelete(false)}>
        <ModalHeader>Excluir Transação</ModalHeader>
        <ModalBody>
          Deseja excluir <strong>{transacaoSelecionada?.descricao}</strong>?
        </ModalBody>
        <ModalFooter>
          <Button color="danger" onClick={excluirTransacao}>Excluir</Button>
          <Button color="secondary" onClick={() => setModalDelete(false)}>Cancelar</Button>
        </ModalFooter>
      </Modal>
    </div>
  );
}
