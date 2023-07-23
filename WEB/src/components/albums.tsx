import { usePhotoAlbumContext } from 'src/context/photo-album.context';

function Albums() {
  const { albums, selectedAlbum, chooseAlbum } = usePhotoAlbumContext();

  return (
    <>
      <div className="card">
        <select data-testid="albums-select"
          value={ selectedAlbum?.id }
          onChange={ (e) => chooseAlbum(e.target.value) }
        >
          <option data-testid="album-option" key={ `option-0` } value="">Choose an Album</option>
          { albums.map((album, i) => (
            <option
              data-testid="album-option"
              key={ `option-${ i }` }
              value={ album.id }
            >Album #{ album.id }</option>
          ))}
        </select>
      </div>
      { selectedAlbum && (
        <div className="card">
          <h3 data-testid="selected-album-title">{ selectedAlbum.title }</h3>
        </div>
      )}
    </>
  )
}
export default Albums;
