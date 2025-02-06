/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    container: {
      center: "true",
      padding: "1rem",
      margin: "0 auto",
      screens: {
        xl: "1400px",
      },
      extend: {
        colors: {
          input: {
            DEFAULT: "hsl(var(--bg-input-color))",
            dark: "hsl(var(--bg-input-color-dark))",
          },
          icon: {
            DEFAULT: "hsl(var(--secondary-color-icon))",
            dark: "hsl(var(--secondary-color-icon-dark))",
          },
        },
      },
    },
    plugins: [],
  },
};
