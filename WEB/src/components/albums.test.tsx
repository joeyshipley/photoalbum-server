import { describe, it, expect } from 'vitest';
import { render, screen, fireEvent } from '@testing-library/react';
import { defaultContextSettings, PhotoAlbumContext } from 'src/context/photo-album.context';
import Albums from 'src/components/albums';

describe('Albums', () => {
  it('Default values without external data.', () => {
    render(
      <PhotoAlbumContext.Provider value={{ ...defaultContextSettings() }}>
        <Albums />
      </PhotoAlbumContext.Provider>
    );
    const albumOptions = screen.getAllByTestId('album-option');

    expect(albumOptions.length).toBe(1);

    const option = albumOptions[0];
    expect(option.value).toBe('');
    expect(option.text).toBe('Choose an Album');
  });

  it('Renders the album select options.', () => {
    render(
      <PhotoAlbumContext.Provider value={{ ...defaultContextSettings(), albums: [ { id: 101, title: 'My Album' }, { id: 202, title: 'My Album' } ] }}>
        <Albums />
      </PhotoAlbumContext.Provider>
    );
    const albumOptions = screen.getAllByTestId('album-option');

    expect(albumOptions.length).toBe(3);

    expect(albumOptions[0].value).toBe('');
    expect(albumOptions[0].text).toBe('Choose an Album');
    expect(albumOptions[1].value).toBe('101');
    expect(albumOptions[1].text).toBe('Album #101');
    expect(albumOptions[2].value).toBe('202');
    expect(albumOptions[2].text).toBe('Album #202');
  });

  it('Calls chooseAlbum with correct id when album is selected.', () => {
    let selectedAlbumId = -1;

    render(
      <PhotoAlbumContext.Provider value={{ ...defaultContextSettings(), albums: [ { id: 101, title: 'My Album' } ], chooseAlbum: (value) => { selectedAlbumId = value } }}>
        <Albums />
      </PhotoAlbumContext.Provider>
    );
    const selectbox = screen.getByTestId('albums-select');
    fireEvent.change(selectbox, { target: { value: 101 } });

    expect(selectedAlbumId).toBe('101');
  });

  it('Renders the selected album.', () => {
    render(
      <PhotoAlbumContext.Provider value={{ ...defaultContextSettings(), selectedAlbum: { id: 101, title: 'My Album' } }}>
        <Albums />
      </PhotoAlbumContext.Provider>
    );
    const title = screen.getByTestId('selected-album-title');
    expect(title).toHaveTextContent('My Album');
  });
});