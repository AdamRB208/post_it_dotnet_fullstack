<script setup>
import { AppState } from '@/AppState.js';
import ModalComponent from '@/components/ModalComponent.vue';
import PictureCard from '@/components/PictureCard.vue';
import PictureForm from '@/components/PictureForm.vue';
import { albumsService } from '@/services/AlbumsService.js';
import { picturesService } from '@/services/PicturesService.js';
import { watchersService } from '@/services/WatchersService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted, watchEffect } from 'vue';
import { useRoute, useRouter } from 'vue-router';


const album = computed(() => AppState.activeAlbum)
const account = computed(() => AppState.account)
const picture = computed(() => AppState.pictures)
const watcherProfile = computed(() => AppState.WatcherProfiles)

const isWatching = computed(() => {
  if (!account.value?.id) {
    logger.log('No account ID found')
    return false
  }

  if (!watcherProfile.value || watcherProfile.value.length === 0) {
    logger.log('Watchers not loaded yet or no watchers found')
    return false
  }

  try {
    const foundWatcher = watcherProfile.value.find(watcher => {
      logger.log('Checking watcher:', watcher)
      return watcher?.id === account.value.id
    })

    const result = !!foundWatcher
    logger.log('isWatching result:', result)
    return result

  } catch (error) {
    logger.error('Error in isWatching:', error)
    return false
  }
})

const route = useRoute()
const router = useRouter()

onMounted(() => {
  getAlbumById()
  getPicturesByAlbumId()
})

watchEffect(() => {
  if (account.value?.id && route.params.albumId) {
    getWatchersByAlbumId()
  }
})

async function getAlbumById() {
  try {
    const albumId = route.params.albumId
    await albumsService.getAlbumById(albumId)
  }
  catch (error) {
    Pop.error(error, 'Could not get Album by Id!')
    logger.error('COULD NOT GET ALBUM BY ID!', error)
  }
}

async function archiveAlbum() {
  try {
    const confirmed = await Pop.confirm(`Are you sure you want to ${album.value.archived ? 'unarchive' : 'archive'} ${album.value.title}?`)
    if (!confirmed) {
      return
    }
    const albumId = route.params.albumId
    await albumsService.archiveAlbum(albumId)
  }
  catch (error) {
    Pop.error(error, 'Unable to archive Album');
    logger.error('UNABLE TO ARCHIVE ALBUM!', error)
  }
}

async function deleteAlbum() {
  try {
    const confirmed = await Pop.confirm(`Are you sure you want to delete ${album.value.title}?`, 'It will be forever.')
    if (!confirmed) {
      return
    }
    const albumId = route.params.albumId
    await albumsService.deleteAlbum(albumId)
    Pop.toast('That album has been deleted!')
    router.push({ name: 'Home' })
  }
  catch (error) {
    Pop.error(error, 'Unable to archive Album');
    logger.error('UNABLE TO DELETE ALBUM', error)
  }
}

async function getPicturesByAlbumId() {
  try {
    const albumId = route.params.albumId
    await picturesService.getPicturesByAlbumId(albumId)
  }
  catch (error) {
    Pop.error(error, 'Could not get Picture by Album Id!');
    logger.error('COULD NOT GET PICTURE BY ALBUM ID!', error);
  }
}

async function getWatchersByAlbumId() {
  try {
    const albumId = route.params.albumId
    await watchersService.getWatchersByAlbumId(albumId)
  }
  catch (error) {
    logger.error('Could not get watchers by Album Id', error)
    Pop.error(error, 'COULD NOT GET WATCHERS BY ALBUM ID');
  }
}

async function createWatcher() {
  try {
    const watcherData = { albumId: route.params.albumId }
    await watchersService.createWatcher(watcherData)
  }
  catch (error) {
    Pop.error(error, 'Could not create Watcher!');
    logger.log('COULD NOT CREATE WATCHER!', error)
  }
}

</script>


<template>
  <section v-if="album" class="container-fluid page-bg">
    <div class="row">
      <div class="col-md-12">
        <div class="d-flex align-items-end mt-3 p-3 rounded shadow album-title-sec"
          :style="{ backgroundImage: `url(${album.coverImg})` }">
          <div class="rounded p-2 flex-grow-1 details-sec">
            <div>
              <h1 class="text-center">{{ album.title }}</h1>
              <p class="text-center">{{ album.description }}</p>
            </div>
            <div class="d-flex justify-content-between align-items-end">
              <div class="d-flex gap-2">
                <div class="text-center rounded-pill bg-postItBlue p-2">{{ album.category }}</div>
                <button @click="archiveAlbum()" v-if="album.creatorId == account?.id"
                  class="btn btn-postItPurple rounded-pill">{{ album.archived ? 'Unarchive Album ' : 'Archive Album '
                  }}<span class="mdi"
                    :class="album.archived ? 'mdi-lock-open-outline' : 'mdi-lock-outline'"></span></button>
                <button @click="deleteAlbum()" v-if="album.creatorId == account?.id" class="btn btn-danger rounded-pill"
                  type="button">Delete Album <i class="mdi mdi-delete"></i></button>
              </div>
              <div class="d-flex gap-2 align-items-end">
                <span>created by {{ album.creator.name }}</span>
                <img :src="album.creator.picture" :alt="album.creator.name" class="rounded-circle creator-img">
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row mt-3">
      <div class="col-md-3">
        <div class="d-flex mb-3 text-light">
          <div class="rounded p-2 flex-grow-1 watchers-sec">
            <span class="d-block">{{ album.watcherCount }}</span>
            <span> Watchers</span>
          </div>
          <button v-if="account" @click="createWatcher()" class="btn btn-vue">
            <span class="mdi mdi-account-plus d-block"></span>
            <span>Join</span>
          </button>
        </div>
        <div v-if="isWatching" class="text-light">
          <p>You are watching this album!</p>
        </div>
        <div class="watcher-albums d-flex flex-wrap gap-2">
          <div v-for="watcher in watcherProfile" :key="watcher.id" class="p-1">
            <img :src="watcher.picture" :alt="watcher.name" class="watcher-album-img rounded mb-2">
          </div>
        </div>
      </div>
      <div class="col-md-9">
        <div class="text-light masonry-container">
          <div v-for="picture in picture" :key="picture.id" class="mb-3">
            <PictureCard :picture="picture" />
          </div>
        </div>
      </div>
      <div v-if="account" class="d-flex justify-content-end">
        <button class="btn btn-vue rounded-pill text-light mb-3" type="button" data-bs-toggle="modal"
          data-bs-target="#pictureModal"><i class="mdi mdi-plus"></i>Add Picture</button>
        <ModalComponent :modal-title="'Create Picture'" :modal-id="'pictureModal'">
          <PictureForm />
        </ModalComponent>
      </div>
    </div>
  </section>
</template>


<style lang="scss" scoped>
.page-bg {
  background-image: url('https://media.istockphoto.com/id/1151198184/photo/modern-wave-effect-3d-red-background.jpg?s=612x612&w=0&k=20&c=IUBzZleWA-IkaGyFrnxd-U9M1L_PfRu8KHCQU7FfeH0=');
  background-size: cover;
  background-position: center;
  background-attachment: fixed;
  min-height: 100dvh;
}
.album-title-sec {
  min-height: 60dvh;
  background-size: cover;
  background-position: center;
}

.details-sec {
  background-color: rgb(33 37 41 / 80%);
  color: white;
}
.watchers-sec {
  background-color: rgb(33 37 41 / 80%);
}
.creator-img {
  height: 4rem;
}
.masonry-container {
  columns: 200px;
}

.masonry-container>* {
  display: inline-block;
  break-inside: avoid;
}
.watcher-album {
  width: 30%;
}

.watcher-album-img {
  width: 100%;
  aspect-ratio: 1/1;
}
</style>