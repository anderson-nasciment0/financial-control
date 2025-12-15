import { useEffect, useState } from "react";
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Table, Input, Label } from "reactstrap";
import { api } from "../../service/api";
import { Pessoa } from "../../models/Pessoa";
import "./PessoasPage.css";

export default function PessoasPage() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [pessoaSelecionada, setPessoaSelecionada] = useState<Pessoa | null>(null);

  const [modalCreate, setModalCreate] = useState(false);
  const [modalEdit, setModalEdit] = useState(false);
  const [modalDelete, setModalDelete] = useState(false);

  const [nome, setNome] = useState("");
  const [idade, setIdade] = useState<number>(0);

  useEffect(() => {
    carregarPessoas();
  }, []);

  async function carregarPessoas() {
    const response = await api.get<Pessoa[]>("/pessoas");
    setPessoas(response.data);
  }

  //função para criar
  async function criarPessoa() {
    await api.post("/pessoas", { nome, idade });
    setModalCreate(false);
    carregarPessoas();
  }

  //função para abrir modal do editar
  function abrirEditar(pessoa: Pessoa) {
    setPessoaSelecionada(pessoa);
    setNome(pessoa.nome);
    setIdade(pessoa.idade);
    setModalEdit(true);
  }

  //função para editar
  async function editarPessoa() {
    await api.put(`/pessoas/${pessoaSelecionada?.pessoaId}`, {
      pessoaId: pessoaSelecionada?.pessoaId,
      nome,
      idade
    });
    setModalEdit(false);
    carregarPessoas();
  }

  //função para abrir modal do excluir
  function abrirExcluir(pessoa: Pessoa) {
    setPessoaSelecionada(pessoa);
    setModalDelete(true);
  }

  //função para excluir
  async function excluirPessoa() {
    await api.delete(`/pessoas/${pessoaSelecionada?.pessoaId}`);
    setModalDelete(false);
    carregarPessoas();
  }

  return (
    <div className="pessoas-container">
      <h2>Pessoas</h2>

      <Button color="success" onClick={() => setModalCreate(true)}>
        Nova Pessoa
      </Button>

      <Table striped className="mt-3">
        <thead>
          <tr>
            <th>Nome</th>
            <th>Idade</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {pessoas.map(p => (
            <tr key={p.pessoaId}>
              <td>{p.nome}</td>
              <td>{p.idade}</td>
              <td>
                <Button size="sm" color="warning" onClick={() => abrirEditar(p)}>
                  Editar
                </Button>{" "}
                <Button size="sm" color="danger" onClick={() => abrirExcluir(p)}>
                  Excluir
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>

      <Modal isOpen={modalCreate} toggle={() => setModalCreate(false)}>
        <ModalHeader>Nova Pessoa</ModalHeader>
        <ModalBody>
          <Label>Nome</Label>
          <Input value={nome} onChange={e => setNome(e.target.value)} />
          <Label className="mt-2">Idade</Label>
          <Input
            type="number"
            value={idade}
            onChange={e => setIdade(Number(e.target.value))}
          />
        </ModalBody>
        <ModalFooter>
          <Button color="primary" onClick={criarPessoa}>Salvar</Button>
          <Button color="secondary" onClick={() => setModalCreate(false)}>Cancelar</Button>
        </ModalFooter>
      </Modal>

      <Modal isOpen={modalEdit} toggle={() => setModalEdit(false)}>
        <ModalHeader>Editar Pessoa</ModalHeader>
        <ModalBody>
          <Label>Nome</Label>
          <Input value={nome} onChange={e => setNome(e.target.value)} />
          <Label className="mt-2">Idade</Label>
          <Input
            type="number"
            value={idade}
            onChange={e => setIdade(Number(e.target.value))}
          />
        </ModalBody>
        <ModalFooter>
          <Button color="warning" onClick={editarPessoa}>Atualizar</Button>
          <Button color="secondary" onClick={() => setModalEdit(false)}>Cancelar</Button>
        </ModalFooter>
      </Modal>

      <Modal isOpen={modalDelete} toggle={() => setModalDelete(false)}>
        <ModalHeader>Excluir Pessoa</ModalHeader>
        <ModalBody>
          Deseja excluir <strong>{pessoaSelecionada?.nome}</strong>?
        </ModalBody>
        <ModalFooter>
          <Button color="danger" onClick={excluirPessoa}>Excluir</Button>
          <Button color="secondary" onClick={() => setModalDelete(false)}>Cancelar</Button>
        </ModalFooter>
      </Modal>
    </div>
  );
}
