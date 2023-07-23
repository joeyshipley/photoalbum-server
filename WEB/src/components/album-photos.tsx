import { usePhotoAlbumContext } from 'src/context/photo-album.context';

function AlbumPhotos() {
  const { albumPhotos, choosePhoto } = usePhotoAlbumContext();

  return (
    <div className="card album-photos-container">
      { albumPhotos.map((photo, i) => (
        <div
          key={ `photo-${ i }` }
          data-testid="album-photo"
          className="album-photo"
          onClick={ () => choosePhoto(photo.id) }
        >
          <img className="thumbnail" src={ photo.thumbnailUrl } />
          <span className="title">Photo #{ photo.id }</span>
        </div>
      ))}
    </div>
  );
}
export default AlbumPhotos;
