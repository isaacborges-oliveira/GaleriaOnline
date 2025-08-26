import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Home } from "../src/pages/home/Home.jsx";
import { Galeria } from "../src/pages/galeria/Galeria.jsx";

export const AppRoutes = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/galeria" element={<Galeria />} />
      </Routes>
    </BrowserRouter>
  );
};
