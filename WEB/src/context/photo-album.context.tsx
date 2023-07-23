/* eslint-disable @typescript-eslint/ban-types */
import React, { createContext, useContext, useState, useEffect } from 'react';
import photoAlbumService from 'src/service/photo-album.service';
import { Album, AlbumPhoto, PhotoDetails } from 'src/service/service.types';

type ContextSettings = {
  isLoading: boolean,
  errors: [],
  albums: Album[],
  selectedAlbum: Album | undefined,
  albumPhotos: AlbumPhoto[],
  selectedPhoto: PhotoDetails | undefined,

  chooseAlbum: Function,
  choosePhoto: Function,
};
export const defaultContextSettings = (): ContextSettings => {
  return {
    isLoading: false,
    errors: [],
    albums: [],
    selectedAlbum: undefined,
    albumPhotos: [],
    selectedPhoto: undefined,

    chooseAlbum: () => {},
    choosePhoto: () => {},
  };
};

export const PhotoAlbumContext = createContext(defaultContextSettings());

export const PhotoAlbumProvider = ({ children }) => {
  const [ isLoading, setIsLoading ] = useState<boolean>(false);
  const [ errors, setErrors ] = useState<[]>([]);
  const [ albums, setAlbums ] = useState<Album[]>([]);
  const [ albumPhotos, setAlbumPhotos ] = useState<AlbumPhoto[]>([]);
  const [ selectedAlbum, setSelectedAlbum ] = useState<Album | undefined>(undefined);
  const [ selectedPhoto, setSelectedPhoto ] = useState<PhotoDetails | undefined>(undefined);

  useEffect(() => {
    photoAlbumService.albums()
      .then((response) => {
        setAlbums(response.data?.albums ?? []);
        setErrors(response.errors ?? []);
      });
  }, []);

  const chooseAlbum = async (albumId) => {
    await setIsLoading(true);

    if(!albumId) {
      await setIsLoading(false);
      setSelectedAlbum(undefined);
      setSelectedPhoto(undefined);
      setAlbumPhotos([]);
      return;
    }

    const id = Number(albumId);
    const album = albums.find((a: Album) => a.id === id);
    setSelectedAlbum(album);

    const response = await photoAlbumService.albumPhotos(id);
    await setIsLoading(false);
    setAlbumPhotos(response.data?.photos ?? []);
    setSelectedPhoto(undefined);
    setErrors(response.errors ?? []);
  };

  const choosePhoto = async (photoId) => {
    if(isLoading) return;

    await setIsLoading(true);
    const id = Number(photoId);

    const response = await photoAlbumService.photo(id);
    await setIsLoading(false);
    setSelectedPhoto(response.data?.photo);
    setErrors(response.errors ?? []);
  };

  const contextData = {
    ...defaultContextSettings(),

    isLoading,
    errors,
    albums,
    selectedAlbum,
    albumPhotos,
    selectedPhoto,

    chooseAlbum,
    choosePhoto,
  };
  return (
    <PhotoAlbumContext.Provider
      value={contextData}
    >
      {children}
    </PhotoAlbumContext.Provider>
  );
};

export const usePhotoAlbumContext = () => useContext(PhotoAlbumContext);
