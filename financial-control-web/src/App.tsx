// App.tsx
import { BrowserRouter, Routes, Route } from "react-router-dom"
import Menu from "./components/Menu"
import PessoasPage from "./pages/Pessoas/PessoasPage"
import CategoriasPage from "./pages/Categorias/CategoriasPage"
import TransacoesPage from "./pages/Transacoes/transacoesPage"
import RelatorioPessoasPage from "./pages/Relatorios/RelatorioPessoasPage"
import RelatorioCategoriasPage from "./pages/Relatorios/RelatorioCategoriasPage"

function App() {
  return (
    <BrowserRouter>
      <Menu />
      <Routes>
        <Route index element={<PessoasPage />} />
        <Route path="/pessoas" element={<PessoasPage />} />
        <Route path="/categorias" element={<CategoriasPage />} />
        <Route path="/transacoes" element={<TransacoesPage />} />
        <Route path="/relatorioPessoas" element={<RelatorioPessoasPage />} />
        <Route path="/relatorioCategorias" element={<RelatorioCategoriasPage />} />

      </Routes>
    </BrowserRouter>
  )
}

export default App
