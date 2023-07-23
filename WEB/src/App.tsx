import { usePhotoAlbumContext } from 'src/context/photo-album.context';

import './App.css'
import Albums from 'src/components/albums';
import AlbumPhotos from 'src/components/album-photos';
import PhotoDetails from 'src/components/photo-details';

function App() {
  const { errors } = usePhotoAlbumContext();
  return (
    <div className="app-container">
      <h1>Photo Album</h1>
      { errors.map((error, i) => (
        <div data-testid="error" key={ `error-${ i }` }>{ error }</div>
      ))}
      <Albums />

      <div className="simple-columns">
        <section className="half">
          <AlbumPhotos />
        </section>
        <section className="half">
          <PhotoDetails />
        </section>
      </div>
    </div>
  )
}
export default App;
