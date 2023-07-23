import { usePhotoAlbumContext } from 'src/context/photo-album.context';

function AlbumPhotos() {
  const { albumPhotos } = usePhotoAlbumContext();

  return (
    <div className="card album-photos-container">
      { albumPhotos.map((photo, i) => (
        <div data-testid="album-photo" className="album-photo" key={ `photo-${ i }` }>
          <img className="thumbnail" src={ photo.thumbnailUrl } />
          <span className="title">Photo #{ photo.id }</span>
        </div>
      ))}
    </div>
  );
}
export default AlbumPhotos
