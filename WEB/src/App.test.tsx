import { describe, it, expect } from 'vitest';
import { render, screen } from '@testing-library/react';
import { defaultContextData, PhotoAlbumContext } from 'src/context/photo-album.context';
import App from 'src/App';

describe('App', () => {
  it('Displays no errors when none are present', () => {
    render(
      <PhotoAlbumContext.Provider value={{ ...defaultContextData() }}>
        <App />
      </PhotoAlbumContext.Provider>
    );

    const errors = screen.queryAllByTestId('error');

    expect(errors.length).toBe(0);
  });

  it('Displays Errors when present', () => {
    render(
      <PhotoAlbumContext.Provider value={{ ...defaultContextData(), errors: [ 'Error 1', 'Error 2' ] }}>
        <App />
      </PhotoAlbumContext.Provider>
    );

    const errors = screen.getAllByTestId('error');

    expect(errors.length).toBe(2);
    expect(errors[0]).toHaveTextContent('Error 1');
    expect(errors[1]).toHaveTextContent('Error 2');
  });
});