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
  likePhoto: Function,
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
    likePhoto: () => {},
  };
};

export const PhotoAlbumContext = createContext(defaultContextSettings());

// TODO: revisit
// Consider and explore abstractions that would clean this area up and
// reduce the inline logic. This will not scale with an actual project.
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

  const likePhoto = async (photoId) => {
    const id = Number(photoId);
    const response = await photoAlbumService.likeIt(id);
    const likeResult = response.data?.photoLikeDetails;
    if(likeResult) {
      setSelectedPhoto({ ...selectedPhoto!, likes: likeResult.likes });
    }
    setErrors(response.errors ?? []);
  }

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
    likePhoto,
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
