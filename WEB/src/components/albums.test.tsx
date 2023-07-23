import { describe, it, expect } from 'vitest';
import { render, screen } from '@testing-library/react';
import { defaultContextData, PhotoAlbumContext } from 'src/context/photo-album.context';
import Albums from 'src/components/albums';

describe('Albums', () => {
  it('Default values without external data', () => {
    render(
      <PhotoAlbumContext.Provider value={{ ...defaultContextData() }}>
        <Albums />
      </PhotoAlbumContext.Provider>
    );
    const albumOptions = screen.getAllByTestId('album-option');

    expect(albumOptions.length).toBe(1);

    const option = albumOptions[0];
    expect(option.value).toBe('');
    expect(option.text).toBe('Choose an Album');
  });

  it('Renders context data', () => {
    render(
      <PhotoAlbumContext.Provider value={{ ...defaultContextData(), albums: [ { id: 101 }, { id: 202 } ] }}>
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
});