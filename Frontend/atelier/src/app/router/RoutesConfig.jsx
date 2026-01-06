import Profile from '../../pages/Profile.jsx';
import About from '../../pages/About.jsx';
import Home from '../../pages/Home.jsx';
import Login from '../../pages/Auth/Login.jsx';
import Contacts from '../../pages/Contacts.jsx';
import Cart from '../../pages/Cart.jsx';
import ForgotPassword from '../../pages/Auth/ForgotPassword.jsx';
import Registration from '../../pages/Auth/Registration.jsx';
import EmailConfirmed from '../../pages/Auth/EmailConfirmed.jsx';

export const privateRoutes = [{ path: '/profile', component: <Profile /> }];

export const publicRoutes = [
  { path: '/about', component: <About /> },
  { path: '/home', component: <Home /> },
  { path: '/login', component: <Login /> },
  { path: '/contacts', component: <Contacts /> },
  { path: '/cart', component: <Cart /> },
  { path: '/registration', component: <Registration /> },
  { path: '/forgotpassword', component: <ForgotPassword /> },
  { path: '/emailconfirmed', component: <EmailConfirmed /> },
];
