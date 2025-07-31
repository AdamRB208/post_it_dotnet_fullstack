<script setup>

defineProps({
  showFooter: {type: Boolean, default: false},
  modalTitle: {type: String, required: true},
  modalId: {type: String, required: true}
})

</script>


<template>
  <div class="modal fade" tabindex="-1" :id="modalId" :aria-labelledby="modalId + 'Label'" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content text-dark">
      <div class="modal-header">
        <h5 class="modal-title" :id="modalId + 'Label'">{{ modalTitle }}</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <slot></slot>
      </div>
      <div v-if="showFooter" class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div>
  </div>
</div>
</template>


<style lang="scss" scoped>

</style>