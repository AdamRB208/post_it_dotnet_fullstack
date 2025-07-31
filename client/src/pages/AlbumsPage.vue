<script setup>
import { AppState } from '@/AppState.js';
import { albumsService } from '@/services/AlbumsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';


const album = computed(() => AppState.activeAlbum)

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
</script>


<template>
  <section v-if="album" class="container-fluid">
    <div class="row">
      <div class="col-md-12">
        <div class="d-flex align-items-end mt-3 p-3 rounded shadow album-title-sec"
          :style="{ backgroundImage: `url(${album.coverImg})` }">
          <div class="rounded p-2 flex-grow-1 details-sec">
            <div>
              <h1 class="text-center">{{ album.title }}</h1>
              <p class="text-center">{{ album.description }}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>


<style lang="scss" scoped>
.album-title-sec {
  min-height: 60dvh;
  background-size: cover;
  background-position: center;
}

.details-sec {
  background-color: rgb(33 37 41 / 80%);
  color: white;
}
</style>