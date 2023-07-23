import { usePhotoAlbumContext } from 'src/context/photo-album.context';

import './App.css'
import Albums from 'src/components/albums';
import AlbumPhotos from 'src/components/album-photos';

function App() {
  const { errors } = usePhotoAlbumContext();
  return (
    <>
      <h1>Photo Album</h1>
      { errors.map((error, i) => (
        <div data-testid="error" key={ `error-${ i }` }>{ error }</div>
      ))}
      <Albums />
      <AlbumPhotos />
    </>
  )
}

export default App
