import js from '@eslint/js';
import reactPlugin from 'eslint-plugin-react';
import reactHooksPlugin from 'eslint-plugin-react-hooks';
import prettierPlugin from 'eslint-plugin-prettier';
import prettierConfig from 'eslint-config-prettier';

export default [
  js.configs.recommended, // Basic JavaScript Rules
  prettierConfig, // Disabling conflicting ESLint rules with Prettier
  {
    ignores: ['node_modules', 'dist'], // Ignore unnecessary folders
  },
  {
    files: ['**/*.js', '**/*.jsx', '**/*.ts', '**/*.tsx'], // Files to check
    languageOptions: {
      sourceType: 'module',
      ecmaVersion: 'latest',
      parserOptions: {
        ecmaFeatures: {
          jsx: true,
        },
      },
      globals: {
        process: 'readonly',
        FileReader: 'readonly',
        URL: 'readonly',
        document: 'readonly',
        window: 'readonly',
        console: 'readonly',
        btoa: 'readonly',
        IntersectionObserver: 'readonly',
      },
    },
    plugins: {
      prettier: prettierPlugin,
      react: reactPlugin,
      'react-hooks': reactHooksPlugin,
    },
    rules: {
      'prettier/prettier': 'error', // Using Prettier
      quotes: ['error', 'single'], // Single quotes
      semi: ['error', 'always'], // Always put `;`
      'no-unused-vars': 'warn', // Warning for unused variables
      'no-console': 'off', // Allow `console.log`
      'react/jsx-uses-react': 'error',
      'react/jsx-uses-vars': 'error',
      'react-hooks/rules-of-hooks': 'error', // Checks hook rules
      'react-hooks/exhaustive-deps': 'warn', // Checks effect dependencies
    },
  },
];
