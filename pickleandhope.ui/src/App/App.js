import React from 'react';
import './App.scss';
import Pickles from '../components/pages/Pickles/Pickles';

function App() {
  return (
    <div className="App">
        <button
          type="button"
          className="btn btn-primary"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </button>
        <Pickles/>
    </div>
  );
}

export default App;
