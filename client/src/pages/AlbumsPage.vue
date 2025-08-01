<script setup>
import { AppState } from '@/AppState.js';
import { albumsService } from '@/services/AlbumsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';


const album = computed(() => AppState.activeAlbum)
const account = computed(() => AppState.account)

const route = useRoute()

onMounted(() => {
  getAlbumById()
})

async function getAlbumById() {
  try {
    const albumId = route.params.albumId
    await albumsService.getAlbumById(albumId)
  }
  catch (error) {
    Pop.error(error, 'Could not get Album by Id!')
    logger.log('COULD NOT GET ALBUM BY ID!', error)
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
    logger.log('UNABLE TO ARCHIVE ALBUM!', error)
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
                <button class="btn btn-danger rounded-pill" type="button">Delete Album <i
                    class="mdi mdi-delete"></i></button>
                <button @click="archiveAlbum()" v-if="album.creatorId === account?.id"
                  class="btn btn-postItPurple rounded-pill">{{ album.archived ? 'Unarchive Album' : 'Archive Album' }}<i
                    class="mdi mdi-lock-outline"></i></button>
                <!-- <button class="btn btn-postItPurple rounded-pill">Un-Archive Album <i
                    class="mdi mdi-lock-open-outline"></i></button> -->
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
        <div class="text-light">
          Watchers here
        </div>
      </div>
      <div class="col-md-9">
        <div class="text-light">
          Pictures here
        </div>
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
.creator-img {
  height: 4rem;
}
</style>