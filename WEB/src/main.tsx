import React from 'react';
import ReactDOM from 'react-dom/client';
import { PhotoAlbumProvider } from 'src/context/photo-album.context';

import App from 'src/App.tsx';
import 'src/index.css';

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <PhotoAlbumProvider>
      <App />
    </PhotoAlbumProvider>
  </React.StrictMode>,
);
