<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import { Pop } from '@/utils/Pop.js';
import { logger } from '@/utils/Logger.js';
import { watchersService } from '@/services/WatchersService.js';

const account = computed(() => AppState.account)
const watcherAlbums = computed(() => AppState.watcherAlbums)

onMounted(() => {
  getMyWatchedAlbums()
})

async function getMyWatchedAlbums() {
  try {
    await watchersService.getMyWatchedAlbums()
  }
  catch (error) {
    Pop.error(error, 'Could not get my watched Albums!');
    logger.log('COULD NOT GET MY WATCHED ALBUMS!', error)
  }
}

</script>

<template>
  <section v-if="account" class="container-fluid page-bg">
    <div class="row about text-center">
      <div class="col-12">
        <div class="mt-5">
          <span class="text-light">Welcome back {{ account.name }}</span>
          <img class="rounded-circle account-img ms-2" :src="account.picture" :alt="account.name" />
          <p class="mt-4 text-light">You are watching {{ watcherAlbums.length }} albums</p>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-md-4">

      </div>
    </div>
  </section>
  <div v-else>
    <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
  </div>
</template>

<style scoped lang="scss">
.account-img {
  height: 5rem;
}

.page-bg {
  background-image: url('https://media.istockphoto.com/id/1151198184/photo/modern-wave-effect-3d-red-background.jpg?s=612x612&w=0&k=20&c=IUBzZleWA-IkaGyFrnxd-U9M1L_PfRu8KHCQU7FfeH0=');
  background-size: cover;
  background-position: center;
  background-attachment: fixed;
  min-height: 100dvh;
}
</style>
