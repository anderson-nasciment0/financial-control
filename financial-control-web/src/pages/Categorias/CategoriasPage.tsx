import { useEffect, useState } from "react";
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Table, Input, Label } from "reactstrap";
import { api } from "../../service/api";
import { Categoria } from "../../models/Categoria";
import "./CategoriasPage.css";

export default function CategoriasPage() {
  const [categorias, setCategorias] = useState<Categoria[]>([]);
  const [categoriaSelecionada, setCategoriaSelecionada] = useState<Categoria | null>(null);

  const [modalCreate, setModalCreate] = useState(false);
  const [modalEdit, setModalEdit] = useState(false);
  const [modalDelete, setModalDelete] = useState(false);

  const [descricao, setDescricao] = useState("");
  const [finalidade, setFinalidade] = useState<number>(1);

  useEffect(() => {
    carregarCategorias();
  }, []);

  async function carregarCategorias() {
    const response = await api.get<Categoria[]>("/categorias");
    setCategorias(response.data);
  }

  //função para criar
  async function criarCategoria() {
    await api.post("/categorias", { descricao, finalidade });
    setModalCreate(false);
    carregarCategorias();
  }

  //função para abrir modal do editar
  function abrirEditar(categoria: Categoria) {
    setCategoriaSelecionada(categoria);
    setDescricao(categoria.descricao);
    setFinalidade(categoria.finalidade);
    setModalEdit(true);
  }

  //função para editar
  async function editarCategoria() {
    await api.put(`/categorias/${categoriaSelecionada?.categoriaId}`, {
      categoriaId: categoriaSelecionada?.categoriaId,
      descricao,
      finalidade
    });
    setModalEdit(false);
    carregarCategorias();
  }

  //função para abrir modal do excluir
  function abrirExcluir(categoria: Categoria) {
    setCategoriaSelecionada(categoria);
    setModalDelete(true);
  }

  //função para excluir
  async function excluirCategoria() {
    try{
    await api.delete(`/categorias/${categoriaSelecionada?.categoriaId}`);
    setModalDelete(false);
    carregarCategorias();
    }catch(error : any){
    if(error.response){
      alert(error.response.data);
    }else{
      alert("Erro inesperado")
    }
  }
  }

  //seleção da finalidade da categoria
  function textoFinalidade(valor: number) {
    switch (valor) {
      case 1: return "Despesa";
      case 2: return "Receita";
      case 3: return "Ambas";
      default: return "";
    }
  }

  return (
    <div className="categorias-container">
      <h2>Categorias</h2>

      <Button color="success" onClick={() => setModalCreate(true)}>
        Nova Categoria
      </Button>

      <Table striped className="mt-3">
        <thead>
          <tr>
            <th>Descrição</th>
            <th>Finalidade</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {categorias.map(c => (
            <tr key={c.categoriaId}>
              <td>{c.descricao}</td>
              <td>{textoFinalidade(c.finalidade)}</td>
              <td>
                <Button size="sm" color="warning" onClick={() => abrirEditar(c)}>
                  Editar
                </Button>{" "}
                <Button size="sm" color="danger" onClick={() => abrirExcluir(c)}>
                  Excluir
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>

      <Modal isOpen={modalCreate} toggle={() => setModalCreate(false)}>
        <ModalHeader>Nova Categoria</ModalHeader>
        <ModalBody>
          <Label>Descrição</Label>
          <Input value={descricao} onChange={e => setDescricao(e.target.value)} />

          <Label className="mt-2">Finalidade</Label>
          <Input type="select" value={finalidade} onChange={e => setFinalidade(Number(e.target.value))}>
            <option value={1}>Despesa</option>
            <option value={2}>Receita</option>
            <option value={3}>Ambas</option>
          </Input>
        </ModalBody>
        <ModalFooter>
          <Button color="primary" onClick={criarCategoria}>Salvar</Button>
          <Button color="secondary" onClick={() => setModalCreate(false)}>Cancelar</Button>
        </ModalFooter>
      </Modal>

      <Modal isOpen={modalEdit} toggle={() => setModalEdit(false)}>
        <ModalHeader>Editar Categoria</ModalHeader>
        <ModalBody>
          <Label>Descrição</Label>
          <Input value={descricao} onChange={e => setDescricao(e.target.value)} />

          <Label className="mt-2">Finalidade</Label>
          <Input type="select" value={finalidade} onChange={e => setFinalidade(Number(e.target.value))}>
            <option value={1}>Despesa</option>
            <option value={2}>Receita</option>
            <option value={3}>Ambas</option>
          </Input>
        </ModalBody>
        <ModalFooter>
          <Button color="warning" onClick={editarCategoria}>Atualizar</Button>
          <Button color="secondary" onClick={() => setModalEdit(false)}>Cancelar</Button>
        </ModalFooter>
      </Modal>

      <Modal isOpen={modalDelete} toggle={() => setModalDelete(false)}>
        <ModalHeader>Excluir Categoria</ModalHeader>
        <ModalBody>
          Deseja excluir <strong>{categoriaSelecionada?.descricao}</strong>?
        </ModalBody>
        <ModalFooter>
          <Button color="danger" onClick={excluirCategoria}>Excluir</Button>
          <Button color="secondary" onClick={() => setModalDelete(false)}>Cancelar</Button>
        </ModalFooter>
      </Modal>
    </div>
  );
}
