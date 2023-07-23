import React from 'react';
import ReactDOM from 'react-dom/client';

import {
  QueryClient,
  QueryClientProvider,
} from 'react-query'
import { PhotoAlbumProvider } from 'src/context/photo-album.context';

import App from 'src/App.tsx';
import 'src/index.css';

const queryClient = new QueryClient();
ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <QueryClientProvider client={queryClient}>
      <PhotoAlbumProvider>
        <App />
      </PhotoAlbumProvider>
    </QueryClientProvider>
  </React.StrictMode>,
);
