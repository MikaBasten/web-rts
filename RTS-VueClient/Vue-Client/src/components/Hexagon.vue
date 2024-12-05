<template>
  <div
    class="hexagon"
    :class="{ expanded: isExpanded, disabled: isDisabled }"
    @click="toggleExpand"
  >
    <div class="hexagon-content">
      <slot></slot>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    isDisabled: Boolean,
    initiallyExpanded: Boolean,
  },
  data() {
    return {
      isExpanded: this.initiallyExpanded || false,
    };
  },
  methods: {
    toggleExpand() {
      if (!this.isDisabled) {
        this.isExpanded = !this.isExpanded;
        this.$emit("toggle", this.isExpanded);
      }
    },
  },
};
</script>

<style scoped>
.hexagon {
  width: 100px;
  height: 100px;
  background-color: #333;
  clip-path: polygon(25% 5%, 75% 5%, 100% 50%, 75% 95%, 25% 95%, 0% 50%);
  transition: transform 0.3s ease, background-color 0.3s ease;
  display: flex;
  align-items: center;
  justify-content: center;
  position: relative;
  cursor: pointer;
}

.hexagon-content {
  display: flex;
  align-items: center;
  justify-content: center;
  text-align: center;
}

.hexagon.expanded {
  width: 200px;
  height: 200px;
  transform: scale(1.5);
  cursor: default;
}

.hexagon.disabled {
  background-color: #222;
  cursor: not-allowed;
}
</style>
