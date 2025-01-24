import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import NavBar from './components/NavBar';
import Home from './components/Home';
import Recipes from './components/Recipes';
import About from './components/About';
import { getHealth } from './services/apiService';

//import logo from './logo.svg';
//import './App.css';

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
        </Routes>
      </div>
    </Router>

    
  );
  /*<div className="App">
      <header className="App-header">
        <p>{healthy ? 'Healthy' : 'Not healthy'}</p>
        <NavBar />
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
      
    </div>*/
}

export default App;
