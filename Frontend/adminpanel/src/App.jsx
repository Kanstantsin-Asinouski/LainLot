import Navbar from './components/UI/navbar/Navbar.jsx';
import AppRouter from './components/AppRouter.jsx';
import { BrowserRouter } from 'react-router-dom';
import { AppProvider } from './provider/AppProvider.jsx';
import { Helmet } from 'react-helmet';
import './styles/App.css';
// rsc - create template component

function App() {
  return (
    <AppProvider>
      <BrowserRouter>
        <Helmet>
          <title>{import.meta.env.VITE_WEBSITE_NAME ?? 'LainLot - Admin Panel'}</title>
        </Helmet>
        <Navbar />
        <AppRouter />
      </BrowserRouter>
    </AppProvider>
  );
}

export default App;
