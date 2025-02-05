import { defineConfig } from "vite";

export default defineConfig({
  build: {
    emptyOutDir: true,
    outDir: "wwwroot",
  },
  server: {
    port: 3000,
    cors: true,
  },
});
