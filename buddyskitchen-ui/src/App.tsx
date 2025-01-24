import { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import NavBar from './components/NavBar';
import Home from './components/Home';
import Recipes from './components/recipes/RecipePage';
import About from './components/About';
import ViewRecipe from './components/recipes/ViewRecipe';
import EditRecipe from './components/recipes/EditRecipe';
import AddRecipe from './components/recipes/AddRecipe';
import { getHealth } from './services/apiService';

function App() {
  const [healthy, setHealth] = useState(false);

  const health = async () => {
    const result = await getHealth();
    if (result.status !== 200) return;
    console.log(result);
    setHealth(true);
  };

  useEffect(() => {
    health();
  }, []);

  return (
    <Router>
      <div className='App'>
        <p>{healthy ? 'Healthy' : 'Not healthy'}</p>
        <NavBar />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/recipes" element={<Recipes />} />
          <Route path="/about" element={<About />} />
          <Route path="/recipes/:recipeId" element={<ViewRecipe />} />
          <Route path="/recipes/edit/:recipeId" element={<EditRecipe />} />
          <Route path="/recipes/add" element={<AddRecipe />} />
        </Routes>
      </div>
    </Router>
  );
  
}

export default App;
