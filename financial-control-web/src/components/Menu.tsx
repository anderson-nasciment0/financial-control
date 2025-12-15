import { NavLink } from "react-router-dom";
import "./Menu.css";

export default function Menu() {
  return (
    <nav className="menu">
      <NavLink to="/pessoas">Pessoas</NavLink>
      <NavLink to="/categorias">Categorias</NavLink>
      <NavLink to="/transacoes">Transações</NavLink>
      <NavLink to="/relatorioPessoas">Relatorio de pessoas</NavLink>
      <NavLink to="/relatorioCategorias">Relatorio de Categorias</NavLink>
    </nav>
  );
}
