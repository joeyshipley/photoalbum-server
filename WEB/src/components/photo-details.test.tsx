import { describe, it, expect } from 'vitest';
import {fireEvent, render, screen } from '@testing-library/react';
import { defaultContextSettings, PhotoAlbumContext } from 'src/context/photo-album.context';
import PhotoDetails from 'src/components/photo-details'; import AlbumPhotos from './album-photos';

describe('PhotoDetails', () => {

  it('Does not render things when not supposed to', () => {
    render(
      <PhotoAlbumContext.Provider value={{ ...defaultContextSettings() }}>
        <PhotoDetails />
      </PhotoAlbumContext.Provider>
    );
    const photoElements = screen.queryAllByTestId('photo-detail');
    const loadingElements = screen.queryAllByTestId('loading-message');

    expect(photoElements.length).toBe(0);
    expect(loadingElements.length).toBe(0);
  });

  it('Renders loading message when loading.', () => {
    render(
      <PhotoAlbumContext.Provider value={{
        ...defaultContextSettings(),
        isLoading: true,
      }}>
        <PhotoDetails />
      </PhotoAlbumContext.Provider>
    );
    const loadingElement = screen.getByTestId('loading-message');

    expect(loadingElement).not.toBeNull();
  });

  it('Renders selected photo when present.', () => {
    render(
      <PhotoAlbumContext.Provider value={{
        ...defaultContextSettings(),
        selectedPhoto: {
          id: 1, albumId: 1, title: 'My Photo', likes: undefined,
          thumbnailUrl: 'thumb.jpg', url: 'full.jpg',
        }
      }}>
        <PhotoDetails />
      </PhotoAlbumContext.Provider>
    );
    const photoElement = screen.getByTestId('photo-detail');

    expect(photoElement).not.toBeNull();
  });

  it('Renders default value when no likes present.', () => {
    render(
      <PhotoAlbumContext.Provider value={{
        ...defaultContextSettings(),
        selectedPhoto: {
          id: 1, albumId: 1, title: 'My Photo', likes: undefined,
          thumbnailUrl: 'thumb.jpg', url: 'full.jpg',
        }
      }}>
        <PhotoDetails />
      </PhotoAlbumContext.Provider>
    );
    const likeButton = screen.getByTestId('like-button');

    expect(likeButton).toHaveTextContent('LIKE (0)');
  });

  it('Renders likes when present.', () => {
    render(
      <PhotoAlbumContext.Provider value={{
        ...defaultContextSettings(),
        selectedPhoto: {
          id: 1, albumId: 1, title: 'My Photo', likes: 99,
          thumbnailUrl: 'thumb.jpg', url: 'full.jpg',
        }
      }}>
        <PhotoDetails />
      </PhotoAlbumContext.Provider>
    );
    const likeButton = screen.getByTestId('like-button');

    expect(likeButton).toHaveTextContent('LIKE (99)');
  });

  it('Calls like with correct id when like button is clicked.', () => {
    let selectedId = -1;

    render(
      <PhotoAlbumContext.Provider value={{
        ...defaultContextSettings(),
        selectedPhoto: {
          id: 101, albumId: 1, title: 'My Photo', likes: 99,
          thumbnailUrl: 'thumb.jpg', url: 'full.jpg',
        },
        likePhoto: (value) => { selectedId = value }
      }}>
        <PhotoDetails />
      </PhotoAlbumContext.Provider>
    );
    const likeButton = screen.getByTestId('like-button');

    fireEvent.click(likeButton);

    expect(selectedId).toBe(101);
  });

});