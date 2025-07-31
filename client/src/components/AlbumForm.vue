<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';


const categories = ['aesthetics', 'food', 'games', 'animals', 'vibes', 'misc']

const editableAlbumData = ref({
  title: '',
  coverImg: '',
  description: '',
  category: ''
})

const router = useRouter()



</script>


<template>
  <form>
    <div class="form-floating mb-3">
      <input v-model="editableAlbumData.title" type="text" class="form-control" id="albumTitle" placeholder="Album Title..." minlength="3" maxlength="25" required>
      <label for="albumTitle" class="form-label">Album Title</label>
    </div>
    <div class="form-floating mb-3">
      <input v-model="editableAlbumData.coverImg" type="url" class="form-control" id="albumCoverImg" placeholder="Album's Cover Image..." maxlength="1000" required>
      <label for="albumCoverImg" class="form-label">Album Cover Image</label>
    </div>
    <div class="form-floating mb-3">
      <textarea v-model="editableAlbumData.description" class="form-control" id="albumDescription" rows="3" placeholder="Albums Description..." minlength="15" maxlength="250"></textarea>
      <label for="albumDescription" class="form-label">Album Description</label>
    </div>
    <div class="form-floating mb-3">
      <select v-model="editableAlbumData.category" id="albumCategory" class="form-control" aria-label="Album Category" required>
        <option value="" selected disabled></option>
        <option v-for="category in categories" :key="'option ' + category" :value="category">{{ category }}</option>
      </select>
      <label for="albumCategory" class="form-label">Choose a Category</label>
    </div>
    <div class="text-end">
      <button class="btn btn-primary" type="submit">Submit</button>
    </div>
  </form>
</template>


<style lang="scss" scoped></style>