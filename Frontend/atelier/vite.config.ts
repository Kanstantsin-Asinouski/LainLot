import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react-swc';

export default defineConfig({
  plugins: [react()],
  server: { port: 3001, open: true,
    // proxy: { '/api': { target: 'http://localhost:8040', changeOrigin: true } }
  },
  resolve: {
    alias: {
      "@": "/src",
      "@app": "/src/app",
      "@core": "/src/core",
      "@shared": "/src/shared",
      "@features": "/src/features"
    },
  },
});