import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      // localhost:5173/api -> localhost:3000/,
      // t.ex. localhost:5173/api/products -> localhost:3000/products
      '/api': {
        // url of the backend server
        target: 'http://localhost:5001',
        changeOrigin: true
      
      
      }
    }
  }
})
