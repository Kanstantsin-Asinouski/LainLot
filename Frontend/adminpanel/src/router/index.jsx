import About from '../pages/About.jsx';
import Error from '../pages/Error.jsx';
import Login from '../pages/Login.jsx';
import RecordIdPage from '../pages/RecordIdPage.jsx';
import Records from '../pages/Records.jsx';

export const privateRoutes = [
  { path: '/about', component: <About /> },
  { path: '/records', component: <Records /> },
  { path: '/records/:table/:id', component: <RecordIdPage /> },
  { path: '/error', component: <Error /> },
];

export const publicRoutes = [{ path: '/login', component: <Login /> }];
