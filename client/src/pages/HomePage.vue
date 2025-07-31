<script setup>
import { AppState } from '@/AppState.js';
import { albumsService } from '@/services/AlbumsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted, ref } from 'vue';

const account = computed(() => AppState.account)

const albums = computed(() => {
  if (filterCategory.value == 'all') {
    return AppState.albums
  }
  return AppState.albums.filter(album => album.category == filterCategory.value)
})

const filterCategory = ref('all')

const categories = [
  {
    name: 'all',
    backgroundImg: 'https://media.istockphoto.com/id/1540187952/vector/synthwave-wireframe-net-illustration-abstract-digital-background-80s-90s-retro-futurism.jpg?s=612x612&w=0&k=20&c=48R3hyOfs-KBvHZbsFLzUkihBb2WC4apupVznKyEeIw='
  },
  {
    name: 'aesthetics',
    backgroundImg: 'https://plus.unsplash.com/premium_photo-1686050878751-89499d28d153?w=900&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nzd8fG5hdHVyZXxlbnwwfDB8MHx8fDA%3D'
  },
  {
    name: 'games',
    backgroundImg: 'https://media.istockphoto.com/id/1301429476/vector/old-retro-video-game-background-platform-arcade-game-design.jpg?s=612x612&w=0&k=20&c=wmv4v-HznAjzZYyYrOCdGAVlAsma6RqTmr5qvED7XeU='
  },
  {
    name: 'animals',
    backgroundImg: 'https://media.istockphoto.com/id/964611070/photo/funny-burrowing-owl-athene-cunicularia.jpg?s=612x612&w=0&k=20&c=H1uU0nYj-vUB13lWo19LQeZ7ToMty_fVjR3LD87xqaE='
  },
  {
    name: 'food',
    backgroundImg: 'https://images.unsplash.com/photo-1563379926898-05f4575a45d8?w=900&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NTF8fGZvb2R8ZW58MHwwfDB8fHww'
  },
  {
    name: 'vibes',
    backgroundImg: 'https://images.unsplash.com/photo-1505236858219-8359eb29e329?w=900&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTh8fHBhcnR5fGVufDB8MHwwfHx8MA%3D%3D'
  },
  {
    name: 'misc',
    backgroundImg: 'https://media.istockphoto.com/id/941906114/vector/neon-lines-background-with-glowing-80s-new-retro-vapor-wave-style.jpg?s=612x612&w=0&k=20&c=7x4Bu7JfInISTu1y2EO2E8lTZPVSzMBZWB6SkqcLQZ8='
  },
]

onMounted(() => {
  getAlbums()
})

async function getAlbums() {
  try {
    await albumsService.getAlbums()
  }
  catch (error) {
    Pop.error(error, 'Could Not Get Albums!')
    logger.error('COULD NOT GET ALBUMS!', error)
  }
}

</script>

<template>
  <section class="container-fluid app-bg">
    <div class="row">
      <div class="col-12">
        <div class="border-bottom border-postItBlue">
          <span class="shadow rounded fs-4 text-postItBlue ps-1">Find Your Interests </span>
        </div>
      </div>
    </div>
    <div class="row">
      <div v-for="category in categories" :key="'filter ' + category.name" class="col-md-3">
        <div class="m-2 rounded text-center text-shadow fw-bold fs-3 p-4 category-btn" role="button"
          :style="{ backgroundImage: `url(${category.backgroundImg})` }" :title="`Filter by ${category.name}`">{{
          category.name }}</div>
      </div>
      <div v-if="account" class="col-md-3">
        <div class="m-2 rounded text-center text-shadow fw-bold fs-3 p-4 category-btn create-btn" role="button"
          title="Create new album">
          create album
        </div>
      </div>
      <div class="row">
        <div class="col-12">
          <div class="border-bottom border-postItBlue">
            <span class="shadow rounded fs-4 text-postItBlue ps-1">Popular Albums</span>
          </div>
        </div>
        <div class="col-md-4">
          {{ albums }}
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped lang="scss">
.app-bg {
  // background-image: url('https://images.unsplash.com/photo-1619084904621-a09493e20701?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTQxfHxhbmltYXRlZCUyMHdhbGxwYXBlcnxlbnwwfHwwfHx8Mg%3D%3D');
  // background-image: url('https://media.istockphoto.com/id/1252290971/photo/geometric-colorful-abstract-technology-background.jpg?s=612x612&w=0&k=20&c=lMyOSBGvi9RnffgCMyu8_XgY_jZQZoEnnUtK8ZhKj0M=');
  background-image: url('https://media.istockphoto.com/id/1151198184/photo/modern-wave-effect-3d-red-background.jpg?s=612x612&w=0&k=20&c=IUBzZleWA-IkaGyFrnxd-U9M1L_PfRu8KHCQU7FfeH0=');
  // background-image: url('https://media.istockphoto.com/id/1166376707/photo/abstract-background-of-shining-particles-digital-sparkling-blue-particles-beautiful-blue.jpg?s=612x612&w=0&k=20&c=1OUeSe02VMaPzjudh8ZJDaNBtpr9lhsKAYdTrzyGZX0=');
  background-size: cover;
  background-position: center;
  background-attachment: fixed;
  min-height: 100dvh;
}

.category-btn {
  background-size: cover;
  background-position: center;
  text-shadow: 0 0 3px #242222;

  border-radius: 0 0 25px 25px;
  color: white;
}

.create-btn {
  background-image: url('https://media.istockphoto.com/id/1056445336/photo/neon-sign-on-brick-wall-background-bingo-3d-rendering.jpg?s=612x612&w=0&k=20&c=gAJ1BAmtk7A-pZ2d-I8_VDRwqoNTm9TxkTrO7w6sAJk=');
}

</style>
