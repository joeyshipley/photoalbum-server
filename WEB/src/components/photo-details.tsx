import { usePhotoAlbumContext } from 'src/context/photo-album.context';

function PhotoDetails() {
  const { isLoading, selectedPhoto, likePhoto } = usePhotoAlbumContext();

  if(isLoading)
    return (<div data-testid="loading-message">Loading...</div>)

  if(!selectedPhoto)
    return (<></>);

  return (
    <div className="card photo-details-container" data-testid="photo-detail">
      <div className="title">{ selectedPhoto.title }</div>
      <button
        data-testid="like-button"
        className="like-button"
        onClick={ () => likePhoto(selectedPhoto.id) }
      >LIKE ({ selectedPhoto.likes ?? 0 })</button>
      <img src={ selectedPhoto.url } />
    </div>
  );
}
export default PhotoDetails;
