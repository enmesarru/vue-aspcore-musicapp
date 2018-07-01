import Vue from 'vue'
import Router from 'vue-router'
import GenreList from '@/components/genres/genre-list'
import GenreEdit from '@/components/genres/genre-edit'
import GenreCreate from '@/components/genres/genre-create'
import ArtistList from '@/components/artist/artist-list'
import ArtistEdit from '@/components/artist/artist-edit'
import ArtistCreate from '@/components/artist/artist-create'
import AlbumList from '@/components/album/album-list'
import AlbumEdit from '@/components/album/album-edit'
import AlbumCreate from '@/components/album/album-create'
import TrackCreate from '@/components/track/track-create'
import TrackList from '@/components/track/track-list'
import TrackEdit from '@/components/track/track-edit'
Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {path: '/', component: GenreList},
    {path: '/genre-list', name: 'genre-list', component: GenreList},
    {path: '/genre-edit/:id', name: 'genre-edit', component: GenreEdit, props: true},
    {path: '/genre-create/', name: 'genre-create', component: GenreCreate},
    {path: '/artist-list', name: 'artist-list', component: ArtistList},
    {path: '/artist-edit/:id', name: 'artist-edit', component: ArtistEdit, props: true},
    {path: '/artist-create', name: 'artist-create', component: ArtistCreate},
    {path: '/album-list', name: 'album-list', component: AlbumList},
    {path: '/album-edit/:id', name: 'album-edit', component: AlbumEdit, props: true},
    {path: '/album-create', name: 'album-create', component: AlbumCreate},
    {path: '/track-create', name: 'track-create', component: TrackCreate},
    {path: '/track-list', name: 'track-list', component: TrackList},
    {path: '/track-edit/:id', name: 'track-edit', component: TrackEdit, props: true}
  ]
})
