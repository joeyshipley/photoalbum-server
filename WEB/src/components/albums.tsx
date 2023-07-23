import { usePhotoAlbumContext } from 'src/context/photo-album.context';

function Albums() {
  const { albums } = usePhotoAlbumContext();

  return (
    <div className="card">
      <select data-testid="albums-select">
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
  )
}
export default Albums
